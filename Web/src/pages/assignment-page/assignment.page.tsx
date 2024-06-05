import { Skeleton, Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import AssignmentBreadcrumb from './assignment.breadcrumb';
import AssignmentHead from './assignment.head';
import AssignmentSubmit from './assignment.submit';
import AssignmentSubmitGuard from './assignment.submit-guard';
import SubmittedAssignmentContent from './submitted-assignment.content';
import { useGetAssignmentQuery } from '../../api/assignments.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { TextEditorOutput } from '../../common/typography';
import { SubmittedAssignmentStatus } from '../../models/assignment.interface';
import { DATE_FORMAT } from '../../utils/constants/app.constants';

export default function AssignmentPage() {
  const { courseId, id } = useParams();
  const { t } = useTranslation();

  const { data, isFetching, error } = useGetAssignmentQuery({ id });

  if (isFetching) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <AssignmentHead title={data.title} />
      <AssignmentBreadcrumb courseId={courseId} title={data.title} />

      <TextEditorOutput text={data.description} />

      <Typography.Paragraph type="secondary">
        {t('course_material.max_grade')}: {data.maxGrade}
      </Typography.Paragraph>

      <Typography.Paragraph
        type={
          data.deadline &&
          dayjs(data.deadline).isBefore(new Date(), 'date') &&
          !dayjs(data.deadline).isSame(new Date(), 'date')
            ? 'danger'
            : 'secondary'
        }
      >
        {t('course_material.deadline')}:{' '}
        {data.deadline
          ? dayjs(data.deadline).format(DATE_FORMAT)
          : t('course_material.no_deadline')}
      </Typography.Paragraph>

      {data.submittedAssignment && (
        <SubmittedAssignmentContent
          submittedAssignment={data.submittedAssignment}
        />
      )}

      {(!data.deadline ||
        dayjs(data.deadline).isAfter(new Date(), 'date') ||
        dayjs(data.deadline).isSame(new Date(), 'date') ||
        data.submittedAssignment?.status ===
          SubmittedAssignmentStatus.Graded) && (
        <AssignmentSubmitGuard courseId={courseId}>
          <AssignmentSubmit
            assignmentId={data.assignmentId}
            maxFiles={data.maxFiles}
          />
        </AssignmentSubmitGuard>
      )}
    </>
  );
}
