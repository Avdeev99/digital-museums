import { Component, EventEmitter, Input, OnInit, Optional, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class CustomButtonComponent implements OnInit {
  @Input() @Optional() public label: string;
  @Input() @Optional() public type: string = 'button';
  @Input() @Optional() public color: string = 'primary';
  @Input() @Optional() public isDisabled: boolean = false;
  @Output() public clickEvent: EventEmitter<void> = new EventEmitter();
  public constructor() {}

  public ngOnInit(): void {}
  public onButtonClick(): void {
    this.clickEvent.emit();
  }
}
