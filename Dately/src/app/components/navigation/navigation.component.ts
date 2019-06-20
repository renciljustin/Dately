import { AuthService } from 'src/app/services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  constructor(public authService: AuthService) { }

  ngOnInit() {
  }

  onLogout() {
    this.authService.logout()
      .subscribe(() => {
        console.log('logout successful.');
      });
  }
}
