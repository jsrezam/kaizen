import { Component } from '@angular/core';

@Component({
  selector: 'loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.css']
})
export class LoadingComponent {
  loadingImg =
    'https://cdn.auth0.com/blog/auth0-react-sample/assets/loading.svg';
}
