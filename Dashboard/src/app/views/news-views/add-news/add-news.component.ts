import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ArticleService } from "src/app/services/article.service";

@Component({
  selector: "app-add-news",
  templateUrl: "./add-news.component.html",
  styleUrls: ["./add-news.component.scss"],
})
export class AddNewsComponent implements OnInit {
  BaseImage: any = "";
  FileToUpload: any;
  submitted = false;
  constructor(private articleService: ArticleService) {}
  ngOnInit(): void {}

  // Pattern For Arabic Letters Only
  private ArabicPattern = /^[[ء-ي\s]+$/;
  private ArabicPatternForParagraph = /^[[ء-ي]|\s]|\.|\,+$/;

  Validation = new FormGroup({
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
    return this.Validation.controls.Image.valid;
  }
  get isTitleValid() {
    return this.Validation.controls.Title.valid;
  }
  get isTitleArValid() {
    return this.Validation.controls.TitleAR.valid;
  }
  get isBodyValid() {
    return this.Validation.controls.Body.valid;
  }
  get isBodyArValid() {
    return this.Validation.controls.BodyAr.valid;
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
    if (this.Validation.valid) {
      const formData: FormData = new FormData();
      if (this.FileToUpload) formData.append("Image", this.FileToUpload);
      else return;
      formData.append("Title_EN", this.Validation.controls.Title.value!);
      formData.append("Title_AR", this.Validation.controls.TitleAR.value!);
      formData.append("Description_EN", this.Validation.controls.Body.value!);
      formData.append("Description_AR", this.Validation.controls.BodyAr.value!);
      this.articleService.create(formData).subscribe({
        next: (res) => {},
        error: (e) => console.log(e),
      });
    }
  }
}
