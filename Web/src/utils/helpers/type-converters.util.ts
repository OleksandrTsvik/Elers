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

export function stringToInputNumber(
  value: string | undefined,
  isOnlyPositive: boolean = true,
  isFraction: boolean = false,
  defaultValue: number = 1,
): number {
  if (!value) {
    return defaultValue;
  }

  let result: string = '';

  if (isOnlyPositive && !isFraction) {
    // додатні цілі числа
    result = value.replace(/\D/g, '');
  } else if (!isOnlyPositive && !isFraction) {
    // будь-які цілі числа
    result = value.replace(/[^-\d]|(?<=\d)-/g, '');
  } else if (isOnlyPositive && isFraction) {
    // додатні дробові числа
    const found = value.match(/[0-9]+\.?[0-9]*/);

    if (found) {
      result = found[0];
    }
  } else {
    // будь-які дробові числа
    const found = value.match(/-?[0-9]+\.?[0-9]*/);

    if (found) {
      result = found[0];
    }
  }

  if (!result.length) {
    return defaultValue;
  }

  if (isFraction) {
    return parseFloat(result);
  }

  return parseInt(result);
}
