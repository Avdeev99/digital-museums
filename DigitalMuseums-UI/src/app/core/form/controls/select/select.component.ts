import { Component, OnInit, Input, Optional, EventEmitter, Output, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IOption, IValidatorError } from '../../form.interface';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.scss'],
})
export class CustomSelectComponent implements OnInit, OnDestroy {
  @Input() public options: IOption[];
  @Input() @Optional() public control: FormControl;
  @Input() @Optional() public label: string;
  @Input() @Optional() public validatorErrors: IValidatorError[];
  @Output() public valueChange: EventEmitter<FormControl> = new EventEmitter();
  @Output() public selectOpenChange: EventEmitter<FormControl> = new EventEmitter();
  @Input() @Optional() private value: IOption;
  @Input() @Optional() private disabled: boolean;
  private unsubscribe$: Subject<void> = new Subject();
  public constructor() {}

  public ngOnInit(): void {
    this.control = !!this.control ? this.control : new FormControl(this.value);
    this.control.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(() => {
      this.valueChange.emit(this.control);
    });
  }

  public onOpenedChange(): void {
    this.selectOpenChange.emit(this.control);
  }

  public trackByFn(index: number, item: IOption): string | number {
    return !!item.id ? item.id : index;
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
