import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ArticleService } from "src/app/services/article.service";
import { ArticleEdit } from "src/app/interfaces/ArticleEdit";
import { ActivatedRoute } from "@angular/router";

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
    private articleService: ArticleService
  ) {}
  ngOnInit(): void {
    this.articleService
      .getEditData(this.Route.snapshot.params["id"])
      .subscribe({
        next: (res) => {
          this.new = res;
        },
        error: (e) => console.log(e),
      });
    this.Validation.controls["Title"].value = this.new?.title_EN;
    this.Validation.controls["TitleAR"].value = this.new?.title_AR;
    this.Validation.controls["Body"].value = this.new?.description_EN;
    this.Validation.controls["BodyAR"].value = this.new?.description_AR;

    this.Validation.controls["Title"].status = "VALID";
    this.Validation.controls["TitleAR"].status = "VALID";
    this.Validation.controls["Body"].status = "VALID";
    this.Validation.controls["BodyAR"].status = "VALID";
    this.Validation.controls["Image"].status = "VALID";

    this.Validation.status = "VALID";
  }
  new: ArticleEdit | null = null;
  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;

  Validation: any = new FormGroup({
    Image: new FormControl(null, [Validators.required]),
    Title: new FormControl(null, [
      Validators.required,
      Validators.minLength(10),
    ]),
    TitleAR: new FormControl(null, [
      Validators.required,
      Validators.minLength(10),
      Validators.pattern(this.ArabicPatternForParagraph),
    ]),
    Body: new FormControl(null, [
      Validators.required,
      Validators.minLength(10),
      Validators.maxLength(500),
    ]),
    BodyAr: new FormControl(null, [
      Validators.required,
      Validators.minLength(10),
      Validators.maxLength(500),
      Validators.pattern(this.ArabicPatternForParagraph),
    ]),
  });
  get isImageValid() {
    return this.Validation.controls["Image"].status == "VALID";
  }
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
    this.submitted = true;
    if (this.Validation.valid && this.new) {
      const formData: FormData = new FormData();
      if (this.FileToUpload) formData.append("Image", this.FileToUpload);
      else return;
      formData.append("Title_EN", this.Validation.controls.Title.value!);
      formData.append("Title_AR", this.Validation.controls.TitleAR.value!);
      formData.append("Description_EN", this.Validation.controls.Body.value!);
      formData.append("Description_AR", this.Validation.controls.BodyAr.value!);
      this.articleService.update(this.new!.id, formData).subscribe({
        next: (res) => {},
        error: (e) => console.log(e),
      });
    }
  }
}
