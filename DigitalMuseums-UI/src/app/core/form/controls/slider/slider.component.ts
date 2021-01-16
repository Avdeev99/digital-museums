import { Component, EventEmitter, Input, OnInit, Optional, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSliderChange } from '@angular/material/slider';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.scss'],
})
export class SliderComponent implements OnInit {
  @Input() public maxValue: number;
  @Input() @Optional() public control: FormControl;
  @Input() @Optional() public minValue: number = 0;
  @Input() @Optional() public value: number = 0;
  @Input() @Optional() public showThumbLabel: boolean = false;
  @Input() @Optional() public step: number = 1;
  @Input() @Optional() public inputPosition: 'before' | 'after' = 'after';
  @Output() public valueChange: EventEmitter<FormControl> = new EventEmitter();
  public constructor() {}
  public ngOnInit(): void {
    this.control = !!this.control ? this.control : new FormControl(this.value);
  }
  public onSlideChange(event: MatSliderChange): void {
    this.control.setValue(event.value);
    this.valueChange.emit(this.control);
  }
}
