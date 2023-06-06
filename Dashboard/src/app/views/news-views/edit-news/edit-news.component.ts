import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ArticleService } from "src/app/services/article.service";
import { ArticleEdit } from "src/app/interfaces/ArticleEdit";
import { ActivatedRoute,Router } from "@angular/router";

@Component({
  selector: "app-edit-news",
  templateUrl: "./edit-news.component.html",
  styleUrls: ["./edit-news.component.scss"],
})
export class EditNewsComponent implements OnInit {
  BaseImage: any = "";
  FileToUpload: any;
  submitted = false;
  constructor(
    private Route: ActivatedRoute,
    private articleService: ArticleService,
    private Router:Router
  ) {
    articleService
      .getEditData(Route.snapshot.params["id"])
      .subscribe({
        next: (res) => {
          this.new = res;
        },
        error: (e) => console.log(e),
        complete: () => {
          this.Validation = new FormGroup({
            Title: new FormControl(this.new?.title_EN, [
              Validators.required,
              Validators.minLength(10),
            ]),
            TitleAR: new FormControl(this.new?.title_AR, [
              Validators.required,
              Validators.minLength(10),
              Validators.pattern(RegExp("^[\u0621-\u064A\u0660-\u0669 0-9 a-zA-Z \$&\+,:;=?@#|'<>.^*()%!\-]+\$")),
            ]),
            Body: new FormControl(this.new?.description_EN, [
              Validators.required,
              Validators.minLength(10),
              Validators.maxLength(500),
            ]),
            BodyAr: new FormControl(this.new?.description_AR, [
              Validators.required,
              Validators.minLength(10),
              Validators.maxLength(500),
              Validators.pattern(RegExp("^[\u0621-\u064A\u0660-\u0669 0-9 a-zA-Z \$&\+,:;=?@#|'<>.^*()%!\-]+\$")),
            ]),
          });
      
        }
      });

   
  }
  ngOnInit(): void {

    
    this.Validation.controls["Title"].value = this.new?.title_EN;
    this.Validation.controls["TitleAR"].value = this.new?.title_AR;
    this.Validation.controls["Body"].value = this.new?.description_EN;
    this.Validation.controls["BodyAR"].value = this.new?.description_AR;
    // this.Validation.controls["Title"].value = this.new?.title_EN;
    // this.Validation.controls["TitleAR"].value = this.new?.title_AR;
    // this.Validation.controls["Body"].value = this.new?.description_EN;
    // this.Validation.controls["BodyAR"].value = this.new?.description_AR;

    this.Validation.controls["Title"].status = "VALID";
    this.Validation.controls["TitleAR"].status = "VALID";
    this.Validation.controls["Body"].status = "VALID";
    this.Validation.controls["BodyAR"].status = "VALID";
    this.Validation.controls["Image"].status = "VALID";

    this.Validation.status = "VALID";
  }
  new: ArticleEdit | null = null;

  Validation: any;

  get isTitleValid() {
    return this.Validation.controls["Title"].status == "VALID";
  }
  get isTitleArValid() {
    return this.Validation.controls["TitleAR"].status == "VALID";
  }
  get isBodyValid() {
    return this.Validation.controls["Body"].status == "VALID";
  }
  get isBodyArValid() {
    return this.Validation.controls["BodyAr"].status == "VALID";
  }

  async ChangeSrc(e: any) {
    var file = e.target.files[0];
    if (file) {
      this.BaseImage = await this.ConvertBase64(file);
      this.FileToUpload = file;
    }
  }
  ConvertBase64 = (file: any) => {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();

      fileReader.readAsDataURL(file);
      fileReader.onload = () => resolve(fileReader.result);

      fileReader.onerror = (error) => reject(error);
    });
  };
  onSubmit() {
    if (this.Validation.valid && this.new) {
      this.submitted=true
      const formData: FormData = new FormData();
      
      if (this.FileToUpload) formData.append("Image", this.FileToUpload);
      // else return;
      formData.append("Title_EN", this.Validation.controls.Title.value!);
      formData.append("Title_AR", this.Validation.controls.TitleAR.value!);
      formData.append("Description_EN", this.Validation.controls.Body.value!);
      formData.append("Description_AR", this.Validation.controls.BodyAr.value!);
      this.articleService.update(this.new!.id, formData).subscribe({
        next: () => {alert("News Edited Successfully")
                       this.Router.navigate(['/News'])
      },
        error: (e) => console.log(e),
      });
    }
    else
    alert(`Make Sure To Fill All The Input Fields`)
    console.log(this.Validation)
};
  }

