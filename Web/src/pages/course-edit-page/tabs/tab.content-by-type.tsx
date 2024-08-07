import { Button, Typography } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { TextEditorOutput } from '../../../common/typography';
import { CourseMaterial } from '../../../models/course-material.type';
import {
  CourseMaterialType,
  useDownloadCourseMaterialFile,
} from '../../../shared';
import { DATE_FORMAT } from '../../../utils/constants/app.constants';

interface Props {
  material: CourseMaterial;
}

export default function TabContentByType({ material }: Props) {
  const { t } = useTranslation();
  const { downloadCourseMaterialFile } = useDownloadCourseMaterialFile();

  switch (material.type) {
    case CourseMaterialType.Content:
      return <TextEditorOutput text={material.content} />;
    case CourseMaterialType.Link:
      return (
        <Link to={material.link} target="_blank">
          {material.title}
        </Link>
      );
    case CourseMaterialType.File:
      return (
        <Button
          className="p-0"
          type="link"
          onClick={() =>
            downloadCourseMaterialFile(
              material.uniqueFileName,
              material.fileName,
            )
          }
        >
          {material.title}
        </Button>
      );
    case CourseMaterialType.Assignment:
      return (
        <>
          <Typography.Text>{material.title}</Typography.Text>
          <Typography.Paragraph className="m-0" type="secondary">
            {t('course_material.deadline')}:{' '}
            {material.deadline
              ? dayjs(material.deadline).format(DATE_FORMAT)
              : t('course_material.no_deadline')}
          </Typography.Paragraph>
          <Typography.Paragraph className="m-0" type="secondary">
            {t('course_material.assignment_max_files')}: {material.maxFiles}
          </Typography.Paragraph>
          <Typography.Paragraph className="m-0" type="secondary">
            {t('course_material.max_grade')}: {material.maxGrade}
          </Typography.Paragraph>
        </>
      );
    case CourseMaterialType.Test:
      return (
        <>
          <Typography.Text>{material.title}</Typography.Text>
          <Typography.Paragraph className="m-0" type="secondary">
            {t('course_material.deadline')}:{' '}
            {material.deadline
              ? dayjs(material.deadline).format(DATE_FORMAT)
              : t('course_material.no_deadline')}
          </Typography.Paragraph>
          <Typography.Paragraph className="m-0" type="secondary">
            {t('course_test.number_attempts')}: {material.numberAttempts}
          </Typography.Paragraph>
          {material.timeLimitInMinutes && (
            <Typography.Paragraph className="m-0" type="secondary">
              {t('course_test.time_limit')}: {material.timeLimitInMinutes}
            </Typography.Paragraph>
          )}
        </>
      );
  }
}
