import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PregledDetaljaComponent } from './pregled-detalja.component';

describe('PregledDetaljaComponent', () => {
  let component: PregledDetaljaComponent;
  let fixture: ComponentFixture<PregledDetaljaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PregledDetaljaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PregledDetaljaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
