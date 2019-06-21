import { UsersService } from './../../../services/users.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  constructor(private usersService: UsersService) { }

  ngOnInit() {
    this.usersService.getList(null)
      .subscribe(result => console.log(result));
  }
}
