import { Component, OnInit } from "@angular/core";
import { Article } from "src/app/interfaces/Article";
import { ArticleService } from "src/app/services/article.service";
@Component({
  selector: "app-all-news",
  templateUrl: "./all-news.component.html",
  styleUrls: ["./all-news.component.scss"],
})
export class AllNewsComponent implements OnInit {
  constructor(private articleService: ArticleService) {}
  ngOnInit(): void {
    this.articleService.getAll().subscribe({
      next: (res) => {
        this.news = res;
      },
      error: (e) => console.log(e),
    });
  }
  news: Article[] = [];
  removeNews(Msg: Article) {
    console.log(Msg.title + " deleted");
    this.news = this.news.filter((item) => item.id !== Msg.id);
  }
}
