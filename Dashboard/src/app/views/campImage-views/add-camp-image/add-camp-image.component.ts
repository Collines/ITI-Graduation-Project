import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CampImageService } from 'src/app/services/camp-image.service';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-add-camp-image',
  templateUrl: './add-camp-image.component.html',
  styleUrls: ['./add-camp-image.component.scss']
})
export class AddCampImageComponent implements OnInit {
  FileToUpload:any;
  BaseImage:any = "";
  constructor(private Router:Router,
    private AccountService:AccountService,
    private ImageService:CampImageService){}

    ngOnInit(): void {
      let admin = this.AccountService.getAdmin()
      if(!admin) {
        this.Router.navigate(["/login"]);
      }
    }
    validator = new FormGroup({
    Image: new FormControl(null, [Validators.required])
    });

    get Image_Valid(){return this.validator.controls["Image"].valid;}

    Add(Image:any){
      if(this.validator.valid)
    {
      const formData: FormData = new FormData();
      if (this.FileToUpload) {
        formData.append('Image', this.FileToUpload);
        this.ImageService.AddCampImage(formData).subscribe({
          next: data => {alert("Image Added Successfully")
                          this.Router.navigate(['/CampImages'])},
          error: err => console.log(err)});
      }
    }
  }
  
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
