import { StatusIconPipe } from './status-icon.pipe';

describe('StatusIconPipe', () => {
  it('create an instance', () => {
    const pipe = new StatusIconPipe();
    expect(pipe).toBeTruthy();
  });

  it('Prospect (titlecase) should result in online', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('Prospect');
    expect(x).toEqual('online');
  });
  it('PrOsEct (mixed case) should result in online', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('PrOspEct');
    expect(x).toEqual('online');
  });
  it('prospect (lowercase) should result in online', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('prospect');
    expect(x).toEqual('online');
  });

  it('purchased (lowercase) should result in money', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('purchased');
    expect(x).toEqual('money');
  });

  it('Purchased (titlecase) should result in money', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('Purchased');
    expect(x).toEqual('money');
  });
  it('pUrchased (mixedcase) should result in money', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('pUrchased');
    expect(x).toEqual('money');
  });

  it('null should result in users', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform(null);
    expect(x).toEqual('users');
  });
  it('undefined should result in users', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform(undefined);
    expect(x).toEqual('users');
  });

  it('empty string should result in users', () => {
    const pipe = new StatusIconPipe();
    const x = pipe.transform('');
    expect(x).toEqual('');
  });

});

