import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginUsuarioComponent } from './login-usuario.component';

describe('LoginUsuarioComponent', () => {
  let component: LoginUsuarioComponent;
  let fixture: ComponentFixture<LoginUsuarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginUsuarioComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
