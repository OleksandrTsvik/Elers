import { TableColumnsType } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import MyGradesTableTitle from './my-grades-table.title';
import {
  GetCourseMyGradeItemResponse,
  GradeType,
} from '../../models/grade.interface';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

export default function useMyGradesColumns(
  courseId: string | undefined,
): TableColumnsType<GetCourseMyGradeItemResponse> {
  const { t } = useTranslation();

  return [
    {
      key: 'title',
      width: 1,
      render: (_, { assessment }) => (
        <MyGradesTableTitle courseId={courseId} assessment={assessment} />
      ),
    },
    {
      key: 'grade',
      title: t('assignment.grade'),
      width: 1,
      render: (_, { grade }) => grade?.grade,
    },
    {
      key: 'teacher',
      title: t('user.teacher'),
      width: 1,
      render: (_, { grade }) =>
        (grade?.type === GradeType.Assignment ||
          grade?.type === GradeType.Manual) &&
        grade.teacher && (
          <>
            {grade.teacher.lastName} {grade.teacher.firstName}{' '}
            {grade.teacher.patronymic}
          </>
        ),
    },
    {
      key: 'date',
      title: t('date'),
      width: 1,
      render: (_, { grade }) =>
        grade?.createdAt && dayjs(grade.createdAt).format(DATE_FORMAT),
    },
  ];
}
