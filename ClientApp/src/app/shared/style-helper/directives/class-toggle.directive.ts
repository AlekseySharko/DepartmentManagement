import {Directive, ElementRef, Renderer2, Input} from '@angular/core';

@Directive({
  selector: '[classToggle]'
})
export class ClassToggleDirective {
  constructor(private elementRef: ElementRef) { }
  private toggleStatus!:boolean;
  @Input() toggleClass:string = "";
  @Input('classToggle') set toggleValue(value:boolean) {
    if(!this.toggleClass) return;
    if(this.toggleStatus == undefined) {
      this.toggleStatus = value;
      this.toggle();
      return;
    }
    if(value != this.toggleStatus) {
      this.toggle();
      this.toggleStatus = value;
    }
  }

  toggle() {
    const hasClass = this.elementRef.nativeElement.classList.toggle(this.toggleClass);
  }
}
