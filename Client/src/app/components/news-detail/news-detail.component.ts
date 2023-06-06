import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ArticleService } from './../../Services/article.service';
import { Article } from 'src/app/Interfaces/Article';


@Component({
  selector: 'app-news-detail',
  templateUrl: './news-detail.component.html',
  styleUrls: ['./news-detail.component.css']
})
export class NewsDetailComponent implements OnInit  {
  ID: number;
  article: Article = {
    id: 0,
    title: '',
   description:'',
    image: '',
    postedAt: ''
  };

  constructor(
    private articleService: ArticleService,
    private Route: ActivatedRoute,
    private router: Router,
  ) {
    this.ID = this.Route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.articleService.getById(this.ID).subscribe({
      next: (res) => {
        this.article = res;
      },
      error: (e) => {
        console.log(e);
      },
    });
  }
  }

