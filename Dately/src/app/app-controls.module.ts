import { NgModule } from '@angular/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
@NgModule({
    imports: [
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        ModalModule.forRoot()
    ],
    exports: [
        BsDatepickerModule,
        BsDropdownModule,
        ModalModule
    ]
})
export class AppControlsModule { }
