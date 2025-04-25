import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

// PrimeNG
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { ToolbarModule } from 'primeng/toolbar';
import { DataViewModule } from 'primeng/dataview';
import { IconFieldModule } from 'primeng/iconfield';
import { InputMaskModule } from 'primeng/inputmask';
import { CardModule } from 'primeng/card';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputIconModule } from 'primeng/inputicon';
import { FocusTrapModule } from 'primeng/focustrap';
import { PopoverModule } from 'primeng/popover';
import { AccordionModule } from 'primeng/accordion';
import { FieldsetModule } from 'primeng/fieldset';

@NgModule({
  exports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    InputMaskModule,
    InputTextModule,
    FloatLabelModule,
    PasswordModule,
    CardModule,
    InputGroupModule,
    InputGroupAddonModule,
    IconFieldModule,
    InputIconModule,
    ToolbarModule,
    DataViewModule,
    FocusTrapModule,
    PopoverModule,
    AccordionModule,
    FieldsetModule,
  ],
})
export class UiPrimeModule {}
