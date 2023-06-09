import { BannerService } from 'src/app/services/banner.service';
import { Router, ActivatedRoute} from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/Services/account.service';
@Component({
  selector: 'app-edit-banner',
  templateUrl: './edit-banner.component.html',
  styleUrls: ['./edit-banner.component.scss']
})
export class EditBannerComponent implements OnInit {
  constructor(
    private BannerServ:BannerService,
    private Route:ActivatedRoute,
    private Router:Router,
    private AccountService:AccountService
    )
    {
      this.ID = this.Route.snapshot.params["id"];
    }

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }
    this.BannerServ.GetBannerInsertDTO(this.ID).subscribe({
      next: data => {this.Banner = data;
        this.BaseImage = this.Banner.image
      console.log(data)},
      error: err => console.log(err)
    })

}


  // Pattern For Arabic Letters Only
  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;

  // Options For Gender Select List In Html
  BaseImage:any = "";
  ID:any;
  Banner:any;
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
    Image: new FormControl(null)
  });



  get Title_EN_Valid(){return this.validator.controls["Title_EN"].valid;}
  get Title_AR_Valid(){return this.validator.controls["Title_AR"].valid;}
  get Description_EN_Valid(){return this.validator.controls["Description_EN"].valid;}
  get Description_AR_Valid(){return this.validator.controls["Description_AR"].valid;}
  get Image_Valid(){return this.validator.controls["Image"].valid;}



  Edit (Title_EN:any,Title_AR:any,Description_EN:any,Description_AR:any){
      
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
  formData.append('Title_EN', Title_EN);
  formData.append('Title_AR', Title_AR);
  formData.append('Description_EN', Description_EN);
  formData.append('Description_AR', Description_AR);
  this.BannerServ.EditBanner(this.ID,formData).subscribe({
    next: () => {
      alert("Banner Edited Successfully");
      this.Router.navigate(['/Banners'])},
    error: err => console.log(formData)
  });
}
else
  alert(`Make Sure To Fill All The Input Fields`)
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