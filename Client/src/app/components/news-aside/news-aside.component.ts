import { Component, OnInit } from '@angular/core';
import { ArticleService } from './../../Services/article.service';
import { Article } from 'src/app/Interfaces/Article';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-news-aside',
  templateUrl: './news-aside.component.html',
  styleUrls: ['./news-aside.component.css']
})
export class NewsAsideComponent implements OnInit {
  constructor(
    private ArticleService:ArticleService,
    private Route: ActivatedRoute,
    private router: Router,
  ) {}
  ngOnInit(): void {
    this.ArticleService.getAll().subscribe({
      next:res=> {
        if(res) {
          if(res.length>3){
            for(let i=0; i<3;i++) {
              this.news.push(res[i])
            }
          } else {
            this.news = res;
          }
        }
      },
      error: e => console.log(e)

    })


  }
  langauge = localStorage.getItem('language');
  news:Article[] = [];

}
