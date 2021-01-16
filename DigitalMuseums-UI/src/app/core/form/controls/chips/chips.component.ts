import { Component, OnInit, ElementRef, ViewChild, Input, Optional, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { map, startWith } from 'rxjs/operators';

import { IOption } from '../../form.interface';

@Component({
  selector: 'app-chips',
  templateUrl: './chips.component.html',
  styleUrls: ['./chips.component.scss'],
})
export class CustomChipsComponent implements OnInit {
  public readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  public hiddenControl: FormControl;
  public filteredOptions$: Observable<IOption[]>;
  @Input() @Optional() public control: FormControl;
  @Input() public label: string;
  @Input() public allOptions: IOption[] = [];
  @Input() @Optional() public selectedOptions: IOption[] = [];
  @Output() public inputFocus: EventEmitter<FormControl> = new EventEmitter();
  @ViewChild('chipInput') private chipInput: ElementRef<HTMLInputElement>;

  public constructor() {}

  public ngOnInit(): void {
    this.hiddenControl = new FormControl();
    this.control = !!this.control ? this.control : new FormControl();
    this.filteredOptions$ = this.hiddenControl.valueChanges.pipe(
      startWith(''),
      map((value: string | null): IOption[] =>
        !!value && typeof value === 'string' ? this.filterOptions(value) : this.allOptions.slice()
      )
    );
  }

  public onChipAdd(event: MatChipInputEvent): void {
    const input: HTMLInputElement = event.input;
    const value: string = event.value;

    // Add option
    if ((value || '').trim() && this.selectedOptions.findIndex((opt): boolean => opt.title === value) === -1) {
      this.selectedOptions.push({ title: value.trim() });
      this.control.setValue(this.selectedOptions);
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }

    this.hiddenControl.reset();
  }

  public onChipRemove(option: IOption): void {
    const index: number = this.selectedOptions.findIndex((opt): boolean =>
      !!option.id ? opt.id === option.id : opt.title === option.title
    );
    if (index >= 0) {
      this.selectedOptions.splice(index, 1);
      this.control.setValue(this.selectedOptions);
    }
  }

  public onOptionSelect(event: MatAutocompleteSelectedEvent): void {
    const optionValue: IOption = event.option.value;
    const isNewOption: boolean = this.selectedOptions.findIndex((opt): boolean => opt.id === optionValue.id) === -1;
    if (isNewOption) {
      this.selectedOptions.push(optionValue);
      this.control.setValue(this.selectedOptions);
    }
    this.chipInput.nativeElement.value = '';
    this.hiddenControl.reset();
  }

  public onInputFocus(): void {
    this.inputFocus.emit(this.control);
  }

  public trackByFn(index: number, item: any): string | number {
    return !!item.id ? item.id : index;
  }

  private filterOptions(value: string): IOption[] {
    const filterValue: string = value.toLowerCase();

    return this.allOptions.filter((opt: IOption): boolean => opt.title.toLowerCase().indexOf(filterValue) === 0);
  }
}
