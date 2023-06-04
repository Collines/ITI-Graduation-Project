import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BannerService } from 'src/app/services/banner.service';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-add-banner',
  templateUrl: './add-banner.component.html',
  styleUrls: ['./add-banner.component.scss']
})
export class AddBannerComponent implements OnInit {
  constructor(
    private Router:Router,
    private AccountService:AccountService,
    private BannerServ:BannerService){}
  ngOnInit(){
    let admin = this.AccountService.getAdmin()
      if(!admin) {
        this.Router.navigate(["/login"]);
      }
  }

  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;


  BaseImage:any = "";
  FileToUpload:any;

  validator = new FormGroup({
    Title_EN: new FormControl(null,
      [Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50)]),
    Title_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPatternForParagraph)]),
    Description_EN: new FormControl(null,
      [Validators.required,
        Validators.minLength(10),
        Validators.maxLength(500)]),
    Description_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(10),
        Validators.maxLength(500),
        Validators.pattern(this.ArabicPatternForParagraph)]),
    Image: new FormControl(null, [Validators.required])
  });


  get Title_EN_Valid(){return this.validator.controls["Title_EN"].valid;}
  get Title_AR_Valid(){return this.validator.controls["Title_AR"].valid;}
  get Description_EN_Valid(){return this.validator.controls["Description_EN"].valid;}
  get Description_AR_Valid(){return this.validator.controls["Description_AR"].valid;}
  get Image_Valid(){return this.validator.controls["Image"].valid;}



  Add (Title_EN:any,Title_AR:any,Description_EN:any,Description_AR:any){

if(this.validator.valid)
{
const formData: FormData = new FormData();
if (this.FileToUpload) {
formData.append('Image', this.FileToUpload);
formData.append('Title_EN', Title_EN);
formData.append('Title_AR', Title_AR);
formData.append('Description_EN', Description_EN);
formData.append('Description_AR', Description_AR);


this.BannerServ.AddBanner(formData).subscribe({
next: data => {alert("Banner Added Successfully")
                this.Router.navigate(['/Banners'])},
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
