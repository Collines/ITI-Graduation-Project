import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Article } from "src/app/interfaces/Article";
import { ArticleService } from "src/app/services/article.service";

@Component({
  selector: "app-details-news",
  templateUrl: "./details-news.component.html",
  styleUrls: ["./details-news.component.scss"],
})
export class DetailsNewsComponent implements OnInit {
  constructor(
    private Route: ActivatedRoute,
    private articleService: ArticleService
  ) {}
  ngOnInit(): void {
    this.articleService.getById(this.Route.snapshot.params["id"]).subscribe({
      next: (res) => (this.new = res),
      error: (e) => console.log(e),
    });
  }
  new: Article = {
    id: 0,
    title: "",
    description: "",
    image: "",
    postedAt: "",
  };
}
