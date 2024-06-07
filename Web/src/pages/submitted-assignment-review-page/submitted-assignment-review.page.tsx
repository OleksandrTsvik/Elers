import { Skeleton, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import GradeAssignmentModalButton from './grade-assignment.modal-button';
import SubmittedAssignmentReviewHead from './submitted-assignment-review.head';
import { useGetSubmittedAssignmentQuery } from '../../api/assignments.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { TextEditorOutput } from '../../common/typography';
import { UserAvatar } from '../../components';
import { DeadlineParagraph, SubmittedAssignmentDetails } from '../../shared';

export default function SubmittedAssignmentReviewPage() {
  const { id } = useParams();
  const { t } = useTranslation();

  const { data, isFetching, error } = useGetSubmittedAssignmentQuery({
    submittedAssignmentId: id,
  });

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
      <SubmittedAssignmentReviewHead />

      <Typography.Title level={3}>{data.title}</Typography.Title>

      <TextEditorOutput text={data.description} />

      <Typography.Paragraph type="secondary">
        {t('course_material.max_grade')}: {data.maxGrade}
      </Typography.Paragraph>

      <Typography.Paragraph type="secondary">
        {t('assignment.max_files')}: {data.maxFiles}
      </Typography.Paragraph>

      <DeadlineParagraph deadline={data.deadline} />

      <Typography.Title level={3}>{t('course.student')}</Typography.Title>

      <Typography.Paragraph>
        <UserAvatar className="mr-avatar" url={data.student.avatarUrl} />
        {data.student.lastName} {data.student.firstName}{' '}
        {data.student.patronymic}
      </Typography.Paragraph>

      <SubmittedAssignmentDetails submittedAssignment={data} />

      <GradeAssignmentModalButton
        submittedAssignmentId={data.submittedAssignmentId}
        grade={data.grade}
        comment={data.teacherComment}
        maxGrade={data.maxGrade}
      />
    </>
  );
}
