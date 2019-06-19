import { Directive, Input, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appTextTransform]'
})
export class TextTransformDirective {

  // tslint:disable-next-line:no-input-rename
  @Input('appTextTransform') appTextTransform = 'lowercase';

  constructor(private el: ElementRef) { }

  @HostListener('focus') onfocus() {
    this.onTransform();
  }

  @HostListener('blur') onBlur() {
    this.onTransform();
  }

  @HostListener('keyup') onKeyPress() {
    this.onTransform();
  }

  onTransform() {
    const value = this.el.nativeElement.value as string;

    if (value === null || value === undefined) {
      return;
    }

    if (this.appTextTransform.toLowerCase() === 'lowercase') {
      this.el.nativeElement.value = value.toLowerCase();
    } else {
      this.el.nativeElement.value = value.toUpperCase();
    }
  }
}
