import { Tooltip } from 'antd';
import dayjs from 'dayjs';

import { GradeItemResponse, GradeType } from '../../models/grade.interface';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

interface Props {
  value: GradeItemResponse | undefined;
}

export default function CourseGradesTdContent({ value }: Props) {
  if (!value) {
    return null;
  }

  return (
    <Tooltip
      title={
        <>
          <div>{dayjs(value.createdAt).format(DATE_FORMAT)}</div>
          {value.type === GradeType.Assignment && value.teacher && (
            <div>
              {value.teacher.lastName} {value.teacher.firstName}{' '}
              {value.teacher.patronymic}
            </div>
          )}
        </>
      }
    >
      <span>{value.grade}</span>
    </Tooltip>
  );
}
