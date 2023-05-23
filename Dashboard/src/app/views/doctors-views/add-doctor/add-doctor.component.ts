import { DoctorsService } from 'src/app/services/doctors.service';
import { DepartmentsService } from 'src/app/services/departments.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Gender } from 'src/app/enums/gender';
@Component({
  selector: 'app-add-doctor',
  templateUrl: './add-doctor.component.html',
  styleUrls: ['./add-doctor.component.scss']
})
export class AddDoctorComponent implements OnInit {

  constructor(private DoctorsService:DoctorsService,private DepartmentsService:DepartmentsService, private Router:Router){}

  ngOnInit(): void {
    this.DepartmentsService.GetAllDepartments().subscribe({
      next: data => this.Departments = data,
      error: err => console.log(err)
    })
  }

  // Pattern For Arabic Letters Only
  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;

  // Options For Gender Select List In Html
  Gender = Gender;
  BaseImage:any = "";
  Departments:any;
  FileToUpload:any;

  // Input Form Validation
  validator = new FormGroup({
    FirstName_EN: new FormControl(null,
      [Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50)]),
    FirstName_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPattern)]),
    LastName_EN: new FormControl(null,
      [Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50)]),
    LastName_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPattern)]),
    Gender: new FormControl(2,
      [Validators.required]),
    Title_EN: new FormControl(null,
      [Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50)]),
    Title_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPatternForParagraph)]),
    Bio_EN: new FormControl(null,
      [Validators.required,
        Validators.minLength(10),
        Validators.maxLength(500)]),
    Bio_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(10),
        Validators.maxLength(500),
        Validators.pattern(this.ArabicPatternForParagraph)]),
    DepartmentId: new FormControl(2, [Validators.required]),
    Image: new FormControl(null, [Validators.required])
  });

  // Checks For Inputs Validation
  get FirstName_EN_Valid(){return this.validator.controls["FirstName_EN"].valid;}
  get FirstName_AR_Valid(){return this.validator.controls["FirstName_AR"].valid;}
  get LastName_EN_Valid(){return this.validator.controls["LastName_EN"].valid;}
  get LastName_AR_Valid(){return this.validator.controls["LastName_AR"].valid;}
  get Gender_Valid(){return this.validator.controls["Gender"].valid;}
  get Title_EN_Valid(){return this.validator.controls["Title_EN"].valid;}
  get Title_AR_Valid(){return this.validator.controls["Title_AR"].valid;}
  get Bio_EN_Valid(){return this.validator.controls["Bio_EN"].valid;}
  get Bio_AR_Valid(){return this.validator.controls["Bio_AR"].valid;}
  get DepartmentId_Valid(){return this.validator.controls["DepartmentId"].valid;}
  get Image_Valid(){return this.validator.controls["Image"].valid;}

  // Add Function => Calls The Post Service Of DoctorsService To Add Doctor To DataBase
   Add (FirstName_EN:any,LastName_EN:any,FirstName_AR:any,LastName_AR:any,Gender:any,
              Title_EN:any,Title_AR:any,Bio_EN:any,Bio_AR:any,DepartmentId:any){

    if(this.validator.valid)
    {
      const formData: FormData = new FormData();
      if (this.FileToUpload) {
        formData.append('Image', this.FileToUpload);
        formData.append('FirstName_EN', FirstName_EN.trim());
        formData.append('LastName_EN', LastName_EN.trim());
        formData.append('FirstName_AR', FirstName_AR.trim());
        formData.append('LastName_AR', LastName_AR.trim());
        formData.append('Title_EN', Title_EN);
        formData.append('Title_AR', Title_AR);
        formData.append('Bio_EN', Bio_EN);
        formData.append('Bio_AR', Bio_AR);
        formData.append('Gender', String(Gender));
        formData.append('DepartmentId', String(DepartmentId));


        this.DoctorsService.AddDoctor(formData).subscribe({
          next: data => {alert("Doctor Added Successfully")
                          this.Router.navigate(['/Doctors'])},
          error: err => console.log(err)});
      }
    }
    else
      alert(`Make Sure To Fill All The Input Fields`);
      console.log(this.validator);
  };

  async ChangeSrc(e:any) {
    var file = e.target.files[0];
    if (file)
      {
        this.BaseImage = await this.ConvertBase64(file);
        this.FileToUpload = file;
      }
  }

  ConvertBase64 = (file:any) => {
    return new Promise((resolve, reject) => {
    const fileReader = new FileReader();

    fileReader.readAsDataURL(file);
    fileReader.onload = () => resolve(fileReader.result);

    fileReader.onerror = (error) => reject(error);
    });
  }

}
