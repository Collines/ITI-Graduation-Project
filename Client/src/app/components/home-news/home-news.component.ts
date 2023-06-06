import { Component, Input } from '@angular/core';
import { Article } from 'src/app/Interfaces/Article';

@Component({
  selector: 'app-home-news',
  templateUrl: './home-news.component.html',
  styleUrls: ['./home-news.component.css']
})
export class HomeNewsComponent {
@Input() new:Article|null= null;
}
