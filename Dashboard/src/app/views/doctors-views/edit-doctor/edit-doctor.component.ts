import { DoctorsService } from 'src/app/services/doctors.service';
import { DepartmentsService } from 'src/app/services/departments.service';
import { Router, ActivatedRoute} from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Gender } from 'src/app/enums/gender';
@Component({
  selector: 'app-edit-doctor',
  templateUrl: './edit-doctor.component.html',
  styleUrls: ['./edit-doctor.component.scss']
})
export class EditDoctorComponent {

  constructor(private DoctorsService:DoctorsService,private DepartmentsService:DepartmentsService,private Route:ActivatedRoute, private Router:Router){
    this.ID = this.Route.snapshot.params["id"];
  }
  ngOnInit(): void {
    this.DepartmentsService.GetAllDepartments().subscribe({
      next: data => this.Departments = data,
      error: err => console.log(err)
    })
    this.DoctorsService.GetDoctorInsertDTO(this.ID).subscribe({
      next: data => {this.Doctor = data;
        this.BaseImage = this.Doctor.image.name
      console.log(data)},
      error: err => console.log(err)
    })
  }

  // Pattern For Arabic Letters Only
  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;

  // Options For Gender Select List In Html
  Gender = Gender
  BaseImage:any = "";
  ID:any;
  Doctor:any;
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
    Gender: new FormControl(null,
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
    DepartmentId: new FormControl(null, [Validators.required]),
    Image: new FormControl(null)
  });

  // Checks For Input Validation
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
  Edit (FirstName_EN:any,LastName_EN:any,FirstName_AR:any,LastName_AR:any,Gender:any,
        Title_EN:any,Title_AR:any,Bio_EN:any,Bio_AR:any,DepartmentId:any){
    if(this.validator.valid)
    {
      console.log("true")
      const formData: FormData = new FormData();
      if (this.FileToUpload)
      {
        formData.append('Image', this.FileToUpload);
        console.log("true")
      }

      formData.append('Id', String(this.ID));
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

      this.DoctorsService.EditDoctor(this.ID,formData).subscribe({
        next: data => {alert("Doctor Edited Successfully");
                        this.Router.navigate(['/Doctors'])},
        error: err => console.log(formData)
      });
    }
    else
      alert(`Make Sure To Fill All The Input Fields`)
      console.log(this.validator)
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
