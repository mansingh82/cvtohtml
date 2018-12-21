import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParseDocumentComponent } from './parse-document.component';

describe('ParseDocumentComponent', () => {
  let component: ParseDocumentComponent;
  let fixture: ComponentFixture<ParseDocumentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParseDocumentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParseDocumentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
