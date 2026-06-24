#!/bin/sh
set -e

bucket_name="${S3_BUCKET_NAME:-tasktracker-local}"
region="${AWS_DEFAULT_REGION:-us-east-1}"

awslocal s3 mb "s3://${bucket_name}" --region "${region}" || true
