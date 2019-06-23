import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-user-list-filter-modal',
  templateUrl: './user-list-filter-modal.component.html',
  styleUrls: ['./user-list-filter-modal.component.scss']
})
export class UserListFilterModalComponent implements OnInit {

  userListFilter: any = {};

  @Input() modalRef: BsModalRef;
  @Output() filterChange = new EventEmitter();

  constructor() {}

  ngOnInit() {
    this.initializeFilter();
  }

  initializeFilter() {
    this.resetFilter();

    const userListFilter = JSON.parse(localStorage.getItem('userListFilter'));

    if (userListFilter) {
      this.userListFilter.gender = userListFilter.hasOwnProperty('gender') ? userListFilter.gender : -1;
      this.userListFilter.interest = userListFilter.hasOwnProperty('interest') ? userListFilter.interest : -1;
      this.userListFilter.age = userListFilter.hasOwnProperty('age') ? userListFilter.age : -1;
    }
  }

  applyFilter() {
    if (this.userListFilter.gender === -1) {
      delete this.userListFilter.gender;
    }

    if (this.userListFilter.interest === -1) {
      delete this.userListFilter.interest;
    }

    if (this.userListFilter.age === -1) {
      delete this.userListFilter.age;
    }

    if (Object.keys(this.userListFilter).length === 0) {
      this.removeFilter();
    } else {
      localStorage.setItem('userListFilter', JSON.stringify(this.userListFilter));
    }

    this.filterChange.emit();

    this.close();
  }

  close() {
    this.modalRef.hide();
  }

  removeFilter() {
    localStorage.removeItem('userListFilter');
    this.resetFilter();
  }

  resetFilter() {
    this.userListFilter = {
      gender: -1,
      interest: -1,
      age: -1
    };
  }
}
