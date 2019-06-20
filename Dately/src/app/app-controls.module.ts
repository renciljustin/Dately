import { NgModule } from '@angular/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
    imports: [
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot()
    ],
    exports: [
        BsDatepickerModule,
        BsDropdownModule
    ]
})
export class AppControlsModule { }
