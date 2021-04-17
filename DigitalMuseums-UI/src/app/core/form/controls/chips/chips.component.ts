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
  public filteredOptions$: Observable<string[]>;
  @Input() @Optional() public control: FormControl;
  @Input() public label: string;
  @Input() public allOptions: string[] = [];
  @Input() @Optional() public selectedOptions: string[] = [];
  @Output() public inputFocus: EventEmitter<FormControl> = new EventEmitter();
  @ViewChild('chipInput') private chipInput: ElementRef<HTMLInputElement>;

  public constructor() {}

  public ngOnInit(): void {
    this.hiddenControl = new FormControl();
    this.control = !!this.control ? this.control : new FormControl();

    this.filteredOptions$ = this.hiddenControl.valueChanges.pipe(
      startWith(''),
      map((option: string | null) => option ? this.filterOptions(option) : this.allOptions.slice()));
  }

  public onInputFocus(): void {
    this.inputFocus.emit(this.control);
  }

  onChipAdd(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;
    const isNew: boolean = this.selectedOptions.findIndex(
      (opt): boolean => opt.toLowerCase() === value.toLowerCase()) === -1

    if ((value || '').trim() && isNew) {
      this.selectedOptions.push(value.trim());
      this.control.setValue(this.selectedOptions);
    }

    if (input) {
      input.value = '';
    }

    this.hiddenControl.reset();
  }

  onChipRemove(option: string): void {
    const index = this.selectedOptions.indexOf(option);

    if (index >= 0) {
      this.selectedOptions.splice(index, 1);
      this.control.setValue(this.selectedOptions);
    }
  }

  onOptionSelect(event: MatAutocompleteSelectedEvent): void {
    const optionValue: string = event.option.value;
    const isNewOption: boolean = this.selectedOptions.indexOf(optionValue) === -1;

    if (isNewOption) {
      this.selectedOptions.push(optionValue);
      this.control.setValue(this.selectedOptions);
    }

    this.chipInput.nativeElement.value = '';
    this.hiddenControl.reset();
  }

  private filterOptions(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allOptions.filter(opt => opt.toLowerCase().indexOf(filterValue) === 0);
  }
}
