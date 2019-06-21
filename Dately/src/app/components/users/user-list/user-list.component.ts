import { UserQuery } from './../../../core/queries/user.query';
import { UserResult } from './../../../core/results/user.result';
import { UsersService } from './../../../services/users.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  userResult: UserResult = {
    users: [],
    total: 0
  };

  userQuery: UserQuery = {};

  constructor(private usersService: UsersService) { }

  ngOnInit() {
    this.usersService.getList(this.userQuery)
      .subscribe(result => {
        this.userResult = result;
      });
  }

  onValueChange($event) {
    console.log($event);
  }
}
