import { NgModule } from '@angular/core';
import { TextTransformDirective } from './components/shared/directives/text-transform.directive';

@NgModule({
    declarations: [
        TextTransformDirective
    ],
    exports: [
        TextTransformDirective
    ]
})
export class AppDirectivesModule { }
