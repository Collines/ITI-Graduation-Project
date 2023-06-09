import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DepartmentsService } from 'src/app/services/departments.service';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-add-department',
  templateUrl: './add-department.component.html',
  styleUrls: ['./add-department.component.scss']
})
export class AddDepartmentComponent implements OnInit {
  constructor(
    private myService:DepartmentsService,
    private router:Router,
    private AccountService:AccountService){}

  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.router.navigate(["/login"]);
    }
  }


  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;
  private EnglishPattern = /^[[A-Za-z\s]+$/;
  private EnglishPatternForParagraph = /^[[A-Za-z]|\s]|\.|\,+$/;

  validator = new FormGroup({
    title_EN: new FormControl(null,
      [Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50),
      Validators.pattern(this.EnglishPattern)]),
    title_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPattern)]),
    description_EN: new FormControl(null,
      [Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50),
        Validators.pattern(this.EnglishPatternForParagraph)]),
    description_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPatternForParagraph)]),
    NoOfBeds: new FormControl(0,
      [Validators.required,
        Validators.min(0)])
      });
      get Title_EN_Valid(){return this.validator.controls["title_EN"].valid;}
      get Title_AR_Valid(){return this.validator.controls["title_AR"].valid;}
      get Description_EN_Valid(){return this.validator.controls["description_EN"].valid;}
      get Description_AR_Valid(){return this.validator.controls["description_AR"].valid;}
      get NoOfBeds_Valid(){return this.validator.controls["NoOfBeds"].valid;}

  AddDept(title_EN:any, title_AR:any, description_EN:any, description_AR:any, NoOfBeds:any){
    if(this.validator.valid){
    const formData: FormData = new FormData();
    
        formData.append('title_EN', title_EN.trim());
        formData.append('title_AR', title_AR.trim());
        formData.append('description_EN', description_EN.trim());
        formData.append('description_AR', description_AR.trim());
        formData.append('numberOfBeds', String(NoOfBeds));


    this.myService.AddDepartment(formData).subscribe(
      response=>
      {
        alert("Department has been added successfully")
        this.router.navigate(['/Departments']);
      },
      err=>{alert('Could not add department.')}
    );
  }
else
  alert(`Make Sure To Fill All The Input Fields`);
  console.log(this.validator);
  }
}
