import { UserListFilterModalComponent } from './../user-list-filter-modal/user-list-filter-modal.component';
import { UserQuery } from './../../../core/queries/user.query';
import { UserResult } from './../../../core/results/user.result';
import { UsersService } from './../../../services/users.service';
import { Component, OnInit, OnChanges } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

/*
  TODO: Add Filtering, Sorting and Pagintation
    Filters:
      [ ] Gender
      [ ] Interest
      [ ] Age(Teens=18-30,Adults=31+)
    Sorting:
      [ ] Name
      [ ] Age
      [ ] CreationTime
      [ ] Likes
*/

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {

  bsModalRef: BsModalRef;

  userResult: UserResult = {
    users: [],
    total: 0
  };
  userQuery: UserQuery = {};


  constructor(private modalService: BsModalService, private usersService: UsersService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers(): void {
    this.applyQuery();

    this.usersService.getList(this.userQuery)
      .subscribe(result => {
        this.userResult = result;
      });
  }

  applyQuery() {
    this.userQuery = {};
    this.applyFilter();
  }

  applyFilter() {
    let filter: any = localStorage.getItem('userListFilter');

    if (filter) {
      filter = JSON.parse(filter);
      this.userQuery = Object.assign(filter);
    }
  }

  showFilter() {
    this.bsModalRef = this.modalService.show(UserListFilterModalComponent);
  }

  removeFilter() {
    localStorage.removeItem('userListFilter');
  }

  hasFilter() {
    return localStorage.getItem('userListFilter');
  }

  search() {
    this.getUsers();
  }
}
