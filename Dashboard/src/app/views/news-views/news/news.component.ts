import { Component, OnInit, Input } from "@angular/core";
import { Article } from "src/app/interfaces/Article";

@Component({
  selector: "app-news",
  templateUrl: "./news.component.html",
  styleUrls: ["./news.component.scss"],
})
export class NewsComponent {
  @Input() new: Article = {
    id: 0,
    title: "",
    description: "",
    image: "",
    postedAt: "",
  };
}
