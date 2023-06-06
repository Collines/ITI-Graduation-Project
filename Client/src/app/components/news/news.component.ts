import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Article } from './../../Interfaces/Article';
import { ArticleService } from './../../Services/article.service';
import { AccountService } from 'src/app/Services/account.service';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit{

  constructor(
    private articleService: ArticleService,
    private accountServices: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.articleService.getAll().subscribe({
      next: (res) => {
        this.news = res;
      },
      error: (e) => {
        console.log(e);
      },
    });
  }
  news: Article[] = [];
}
