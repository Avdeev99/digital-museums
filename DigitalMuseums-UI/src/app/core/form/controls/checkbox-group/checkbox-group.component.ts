import { Component, OnInit, EventEmitter, Input, Optional, Output, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { IOption, IOptionChecked, IValidatorError } from '../../form.interface';


@Component({
  selector: 'app-checkbox-group',
  templateUrl: './checkbox-group.component.html',
  styleUrls: ['./checkbox-group.component.scss'],
})
export class CheckboxGroupComponent implements OnInit, OnDestroy {
  @Input() public options: IOptionChecked[];
  @Input() @Optional() public label: string;
  @Input() @Optional() public control: FormControl;
  @Input() @Optional() public checkboxPosition: 'before' | 'after' = 'before';
  @Input() @Optional() public validatorErrors: IValidatorError[];
  @Output() public valueChange: EventEmitter<FormControl> = new EventEmitter();
  @Input() @Optional() private value: IOptionChecked[];
  private unsubscribe$: Subject<void> = new Subject();

  public constructor() {}

  public ngOnInit(): void {
    this.control = !!this.control ? this.control : new FormControl(this.value);
    this.control.valueChanges.pipe(takeUntil(this.unsubscribe$)).subscribe(() => {
      this.valueChange.emit(this.control);
    });
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  public trackByFn(index: number, item: IOption): string | number {
    return !!item.id ? item.id : index;
  }
}