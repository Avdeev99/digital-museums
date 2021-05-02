import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appExhibitionStepContainer]'
})
export class ExhibitionStepContainerDirective {
  constructor(public containerRef: ViewContainerRef) {}
}
