export function isArrayOfStrings(value: unknown): value is string[] {
  return (
    Array.isArray(value) && value.every((item) => typeof item === 'string')
  );
}

export function isArrayOf<T>(
  value: unknown,
  check: (val: unknown) => val is T,
): value is T[] {
  return Array.isArray(value) && value.every(check);
}

export function isObject(value: unknown): value is object {
  return typeof value === 'object' && value !== null && !Array.isArray(value);
}
