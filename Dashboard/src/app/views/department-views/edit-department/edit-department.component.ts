import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute,Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';
import { DepartmentsService } from 'src/app/services/departments.service';

@Component({
  selector: 'app-edit-department',
  templateUrl: './edit-department.component.html',
  styleUrls: ['./edit-department.component.scss']
})
export class EditDepartmentComponent {
  ID:any;
  Department:any;
  constructor(private DepartmentsService:DepartmentsService,
    private Route:ActivatedRoute,
    private Router:Router,
    private AccountService:AccountService)
    {
      this.ID = this.Route.snapshot.params["id"];
    }
    
  ngOnInit(): void {
    let admin = this.AccountService.getAdmin()
    if(!admin) {
      this.Router.navigate(["/login"]);
    }

    this.DepartmentsService.GetDepartmentInsertDTO(this.ID).subscribe({
      next:(data)=>{this.Department = data;},
      error:(err)=>{console.log(err)}
    });
  }
  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;

  validator = new FormGroup({
    title_EN: new FormControl(null,
      [Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50)]),
    title_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPattern)]),
    description_EN: new FormControl(null,
      [Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50)]),
    description_AR: new FormControl(null,
      [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
        Validators.pattern(this.ArabicPatternForParagraph)])
      });
      get Title_EN_Valid(){return this.validator.controls["title_EN"].valid;}
      get Title_AR_Valid(){return this.validator.controls["title_AR"].valid;}
      get Description_EN_Valid(){return this.validator.controls["description_EN"].valid;}
      get Description_AR_Valid(){return this.validator.controls["description_AR"].valid;}


  Edit(title_EN:any, title_AR:any, description_EN:any, description_AR:any){
    if(this.validator.valid)
    {
      const formData: FormData = new FormData();
      formData.append('Id', String(this.ID));
      formData.append('title_EN', title_EN.trim());
      formData.append('title_AR', title_AR.trim());
      formData.append('description_EN', description_EN.trim());
      formData.append('description_AR', description_AR.trim())
      this.DepartmentsService.EditDepartment(this.ID,formData).subscribe({
        next: data => {
          alert("Department Edited Successfully");
          this.Router.navigate(['/Departments'])},
        error: err => console.log(formData)
      });
    }
    else
      alert(`Make Sure To Fill All The Input Fields`)
  };
}

