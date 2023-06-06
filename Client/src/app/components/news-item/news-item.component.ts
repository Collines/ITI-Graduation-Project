import { Component ,Input} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Article } from 'src/app/Interfaces/Article';

@Component({
  selector: 'app-news-item',
  templateUrl: './news-item.component.html',
  styleUrls: ['./news-item.component.css']
})
export class NewsItemComponent {
  constructor(
    private Route: ActivatedRoute,
    private router: Router,

  ) {
    this.new.id = Route.snapshot.params['id'];
  }

  @Input() new: Article = {
    id: 0,
    title: "",
    description: "",
    image: "",
    postedAt: ""

  };

}
