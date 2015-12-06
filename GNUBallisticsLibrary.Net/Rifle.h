#pragma once

using namespace System;
using namespace Sharp::Ballistics::Abstractions;

#define ZERO_RANGE 100
public ref class Rifle : IRifle {
private:
	RifleInfo^ rifleInfo;
	ScopeInfo^ scopeInfo;
	AmmoInfo^ ammoInfo;

	double zerAngle;

public:
	Rifle(RifleInfo^ rifleInfo, ScopeInfo^ scopeInfo, AmmoInfo^ ammoInfo)
	{
		if (rifleInfo == nullptr)
			throw gcnew System::ArgumentNullException("rifleInfo");

		if (rifleInfo->ZeroingConditions == nullptr)
			throw gcnew System::ArgumentNullException("rifleInfo.ZeroingConditions");

		if (scopeInfo == nullptr)
			throw gcnew System::ArgumentNullException("scopeInfo");
		if (ammoInfo == nullptr)
			throw gcnew System::ArgumentNullException("ammoInfo");

		this->rifleInfo = rifleInfo;
		this->scopeInfo = scopeInfo;
		this->ammoInfo = ammoInfo;

		zerAngle = ZeroAngle((int)ammoInfo->DragFunction,
			AtmCorrect(ammoInfo->BC,
				rifleInfo->ZeroingConditions->Altitude,
				rifleInfo->ZeroingConditions->Barometer,
				rifleInfo->ZeroingConditions->Temperature,
				rifleInfo->ZeroingConditions->RelativeHumidity),
			ammoInfo->MuzzleVelocity,
			scopeInfo->Height,
			scopeInfo->ZeroDistance, 0);
	}

	property String^ Name
	{
		String^ get()
		{
			return rifleInfo->Name;
		}
	}

	virtual ShotInfo^ SolveShot(
		double shootingAngle,
		double windSpeed,
		double windAngle,
		int range,
		AtmosphericInfo^ atmInfo);
};
