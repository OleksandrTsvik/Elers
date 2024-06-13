import { DropResult } from 'react-beautiful-dnd';

export function handleDropResult<T>(
  result: DropResult,
  data: T[],
): T[] | undefined {
  const { destination, source } = result;

  if (!destination || destination.index === source.index) {
    return;
  }

  const reorderedData = [...data];

  const [reorderedItem] = reorderedData.splice(source.index, 1);
  reorderedData.splice(destination.index, 0, reorderedItem);

  return reorderedData;
}
