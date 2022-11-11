import { Component } from '@angular/core';
import {TokenService} from "./services/token.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AuctionWebClient';
  isLoggedIn = false;

  constructor(private tokenService: TokenService, private router: Router) {
  }

  ngOnInit() {
    this.isLoggedIn = this.tokenService.isLoggedIn();
  }

  logout(): void {
    this.tokenService.logout();
    this.isLoggedIn = false;
    this.router.navigate(['login']);
    return;
  }
}
