import { Component, OnInit, Input ,Output, EventEmitter} from "@angular/core";
import { Article } from "src/app/interfaces/Article";
import { ArticleService } from './../../../services/article.service';

@Component({
  selector: "app-news",
  templateUrl: "./news.component.html",
  styleUrls: ["./news.component.scss"],
})
export class NewsComponent {

  constructor(private ArticleService: ArticleService){}

  modal: any;

  @Input() new: Article = {
    id: 0,
    title: "",
    description: "",
    image: "",
    postedAt: "",
  };

  @Output() removeNewsEvent = new EventEmitter<Article>();

  DeleteBtnModal(element: any) {
    // this.modal = new window.bootstrap.Modal(element);
    this.modal = element;
    console.log(this.modal);
  }

  NewsIsDeleted() {
    this.removeNewsEvent.emit(this.new);
  }

  DeleteNews(value: number) {
    this.ArticleService.delete(value).subscribe({
      next: () => (this.new = this.RemoveObjectWithId(this.new, value)),
      error: (err) => console.log(err),
      complete: () => {
        this.NewsIsDeleted();
        this.modal.classList.remove("show");
        this.modal.classList.add("d-none");
        document.getElementsByClassName("modal-backdrop")[0].remove();
        document.body.classList.remove("modal-open");
        document.body.classList.add("overflow-auto");
      },
    });
  }
  RemoveObjectWithId(arr: any, id: number) {
    const objWithIdIndex = arr.findIndex((obj: any) => obj.id == id);

    if (objWithIdIndex > -1) {
      arr.splice(objWithIdIndex, 1);
    }
    return arr;
  }
}
