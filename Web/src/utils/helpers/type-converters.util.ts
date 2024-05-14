export function getUniqueArrayItems<T>(array: T[]): T[] {
  return [...new Set(array)];
}

export function stringToNumber(value: string): number | undefined {
  const num = parseFloat(value);

  return isNaN(num) ? undefined : num;
}

export function stringToBoolean(value: string): boolean {
  const lowerCaseValue = value.trim().toLowerCase();

  return lowerCaseValue === 'true';
}
