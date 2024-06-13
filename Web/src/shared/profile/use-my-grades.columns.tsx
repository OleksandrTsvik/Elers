import { Space, TableColumnsType } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import {
  GetCourseMyGradeItemResponse,
  GradeType,
} from '../../models/grade.interface';
import { DATE_FORMAT } from '../../utils/constants/app.constants';
import { CourseMaterialIcon, CourseMaterialType } from '../materials';

const assessmentIcon: { [key in GradeType]: CourseMaterialType } = {
  Assignment: CourseMaterialType.Assignment,
  Test: CourseMaterialType.Test,
};

const assessmentLink: { [key in GradeType]: string } = {
  Assignment: 'assignment',
  Test: 'test',
};

export default function useMyGradesColumns(
  courseId: string | undefined,
): TableColumnsType<GetCourseMyGradeItemResponse> {
  const { t } = useTranslation();

  return [
    {
      key: 'title',
      width: 1,
      render: (_, { assessment }) => (
        <Link
          className="d-block"
          to={`/courses/${courseId}/${assessmentLink[assessment.type]}/${assessment.id}`}
        >
          <Space align="start">
            <CourseMaterialIcon type={assessmentIcon[assessment.type]} />
            {assessment.title}
          </Space>
        </Link>
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
        grade?.type === GradeType.Assignment &&
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
