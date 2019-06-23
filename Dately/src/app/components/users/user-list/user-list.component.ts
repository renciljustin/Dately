import { KeyValuePair } from './../../../core/models/key-value-pair.model';
import { UserQuery } from './../../../core/queries/user.query';
import { UserResult } from './../../../core/results/user.result';
import { UsersService } from './../../../services/users.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ObjectQuery } from 'src/app/core/queries/object.query';
import { ObjectQueryHelper } from 'src/app/shared/helpers/object-query.helper';

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

  queryHelper = new ObjectQueryHelper<UserQuery>();

  objectQuery: ObjectQuery = {};
  userQuery: UserQuery = {};

  searchWorker: NodeJS.Timer;

  sortMap: KeyValuePair[] = [
      { key: 'CreationTime', value: 'Date Joined' },
      { key: 'Name', value: 'Name' },
      { key: 'Age', value: 'Age' },
      { key: 'Popularity', value: 'Popularity' },
    ];
  sortBy: KeyValuePair = { key: 'CreationTime', value: 'Date Joined' };

  modalRef: BsModalRef;

  constructor(private modalService: BsModalService, private usersService: UsersService) { }

  ngOnInit() {
    this.objectQuery.sortBy = this.sortBy.key;
    this.getUsers();
  }

  getUsers(): void {
    this.applyQuery();
    this.usersService.getList(this.userQuery)
      .subscribe(result => {
        this.userResult = result;
      });
  }

  applyQuery(): void {
    const name = this.userQuery.name;

    this.userQuery = {};

    this.userQuery.name = name ? name : '';

    this.applySorting();
    this.applyFiltering();
  }

  searchName(): void {
    clearTimeout(this.searchWorker);

    this.searchWorker = setTimeout(() => {
      this.getUsers();
    }, 2000);
  }

  applySorting() {
    this.userQuery = this.queryHelper.mapObjectQuery(this.objectQuery, this.userQuery);
  }

  changeSortBy(sort: KeyValuePair): void {
    if (this.objectQuery.sortBy !== sort.key) {
      this.sortBy = sort;
      this.objectQuery.sortBy = sort.key;
      this.getUsers();
    }
  }

  sortByAscending(): void {
    if (this.objectQuery.isOrderDescending) {
      this.objectQuery.isOrderDescending = false;
      this.getUsers();
    }
  }

  sortByDescending(): void {
    if (!this.objectQuery.isOrderDescending) {
      this.objectQuery.isOrderDescending = true;
      this.getUsers();
    }
  }

  isSortAscending(): boolean {
    return !this.objectQuery.isOrderDescending;
  }

  applyFiltering(): void {
    let filter: any = localStorage.getItem('userListFilter');

    if (filter) {
      filter = JSON.parse(filter);
      this.userQuery = this.queryHelper.mapObjectQuery(filter, this.userQuery);
    }
  }


  showFilter(filter: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(filter);
  }

  removeFilter(): void {
    localStorage.removeItem('userListFilter');
    this.getUsers();
  }

  hasFilter(): boolean {
    return localStorage.getItem('userListFilter') !== null;
  }

  search(): void {
    clearInterval(this.searchWorker);
    this.getUsers();
  }
}
