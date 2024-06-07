import { Skeleton, Typography } from 'antd';
import { useTranslation } from 'react-i18next';
import { useParams } from 'react-router-dom';

import AssignmentBreadcrumb from './assignment.breadcrumb';
import AssignmentHead from './assignment.head';
import AssignmentSubmit from './assignment.submit';
import AssignmentSubmitGuard from './assignment.submit-guard';
import { useGetAssignmentQuery } from '../../api/assignments.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { TextEditorOutput } from '../../common/typography';
import { DeadlineParagraph, SubmittedAssignmentDetails } from '../../shared';

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

      <DeadlineParagraph deadline={data.deadline} />

      {data.submittedAssignment && (
        <SubmittedAssignmentDetails
          submittedAssignment={data.submittedAssignment}
        />
      )}

      <AssignmentSubmitGuard
        courseId={courseId}
        deadline={data.deadline}
        status={data.submittedAssignment?.status}
      >
        <AssignmentSubmit
          assignmentId={data.assignmentId}
          maxFiles={data.maxFiles}
        />
      </AssignmentSubmitGuard>
    </>
  );
}
