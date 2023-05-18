import { Component } from '@angular/core';
import { config } from './Config/Config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Client';

  Development = config.Development;
}
