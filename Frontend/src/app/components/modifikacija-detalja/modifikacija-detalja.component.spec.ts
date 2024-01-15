import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifikacijaDetaljaComponent } from './modifikacija-detalja.component';

describe('ModifikacijaDetaljaComponent', () => {
  let component: ModifikacijaDetaljaComponent;
  let fixture: ComponentFixture<ModifikacijaDetaljaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModifikacijaDetaljaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModifikacijaDetaljaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
