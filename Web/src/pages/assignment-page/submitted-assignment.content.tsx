import { Button, Typography } from 'antd';
import { BaseType } from 'antd/es/typography/Base';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import { TextEditorOutput } from '../../common/typography';
import useDownloadFile from '../../hooks/use-download-file';
import {
  SubmittedAssignment,
  SubmittedAssignmentStatus,
} from '../../models/assignment.interface';
import { CourseMaterialFileIcon } from '../../shared';
import { DATE_FORMAT } from '../../utils/constants/app.constants';
import { isNumber } from '../../utils/helpers';

const paragraphTypeStatus: { [key in SubmittedAssignmentStatus]: BaseType } = {
  Submitted: 'warning',
  Graded: 'success',
  Resubmit: 'danger',
};

interface Props {
  submittedAssignment: SubmittedAssignment;
}

export default function SubmittedAssignmentContent({
  submittedAssignment,
}: Props) {
  const { t } = useTranslation();
  const { downloadFileOrOpenPdf } = useDownloadFile();

  return (
    <>
      <Typography.Title level={3}>
        {t('assignment.last_uploaded_answer')}
      </Typography.Title>

      <Typography.Paragraph
        type={paragraphTypeStatus[submittedAssignment.status]}
      >
        {t('assignment.status')}:{' '}
        {t([
          `assignment.submitted_status.${submittedAssignment.status}`,
          submittedAssignment.status,
        ])}
      </Typography.Paragraph>

      <Typography.Paragraph>
        {t('assignment.attempt_number')} №: {submittedAssignment.attemptNumber}
      </Typography.Paragraph>

      {isNumber(submittedAssignment.grade) && (
        <Typography.Paragraph>
          {t('assignment.grade')}: {submittedAssignment.grade}
        </Typography.Paragraph>
      )}

      {submittedAssignment.teacher && (
        <Typography.Paragraph>
          {t('assignment.teacher')}: {submittedAssignment.teacher.lastName}{' '}
          {submittedAssignment.teacher.firstName}{' '}
          {submittedAssignment.teacher.patronymic}
        </Typography.Paragraph>
      )}

      {submittedAssignment.text && (
        <TextEditorOutput text={submittedAssignment.text} />
      )}

      {submittedAssignment.files.map((file) => (
        <Button
          key={file.uniqueFileName}
          className="d-block p-0"
          type="link"
          icon={<CourseMaterialFileIcon extension={file.fileName} />}
          onClick={() =>
            downloadFileOrOpenPdf(
              '/assignments/file/download/' + file.uniqueFileName,
              file.fileName,
            )
          }
        >
          {file.fileName}
        </Button>
      ))}

      <Typography.Paragraph>
        {t('assignment.submitted_at')}:{' '}
        {dayjs(submittedAssignment.submittedAt).format(DATE_FORMAT)}
      </Typography.Paragraph>

      {submittedAssignment.teacherComment && (
        <>
          {t('assignment.comment')}: {submittedAssignment.teacherComment}
        </>
      )}
    </>
  );
}
